using Microsoft.Data.SqlClient;
using CandidateTask.Interfaces;
using CandidateTask.Models;

namespace CandidateTask.Services
{
    public class SqlCandidateRepository : ICandidateRepository
    {
        private readonly string _connectionString;

        public SqlCandidateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Candidate> UpsertCandidate(Candidate candidate)
        {
        

            string? ExistingPhoneNumber =null;
            string? ExistingPreferedCallInterval = null;
            string? ExistingLinkedInProfile =null;
            string? ExistingGitHubProfile =null;

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                   await connection.OpenAsync();
                    var sqlCandidate = @"
                        SELECT *
                        FROM Candidates
                        WHERE Email = @Email
                    ";

                    using (var command = new SqlCommand(sqlCandidate, connection))
                    {
                        command.Parameters.AddWithValue("@Email", candidate.Email);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                ExistingPhoneNumber = reader["PhoneNumber"].ToString();
                                ExistingPreferedCallInterval = reader["PreferedCallInterval"].ToString();
                                ExistingLinkedInProfile = reader["LinkedInProfile"].ToString();
                                ExistingGitHubProfile = reader["Comment"].ToString();

                            }
                        }
                    }



                    var sql = @"
                        MERGE INTO Candidates AS target
                        USING (VALUES (@Email)) AS source (Email)
                        ON target.Email = source.Email
                        WHEN MATCHED THEN
                            UPDATE SET 
                                FirstName = @FirstName,
                                LastName = @LastName,
                                PhoneNumber = @PhoneNumber,
                                PreferedCallInterval = @PreferedCallInterval,
                                LinkedInProfile = @LinkedInProfile,
                                GitHubProfile = @GitHubProfile,
                                Comment = @Comment
                        WHEN NOT MATCHED THEN
                            INSERT (FirstName, LastName, PhoneNumber, Email, PreferedCallInterval, LinkedInProfile, GitHubProfile, Comment)
                            VALUES (@FirstName, @LastName, @PhoneNumber, @Email, @PreferedCallInterval, @LinkedInProfile, @GitHubProfile, @Comment);
                    ";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", candidate.FirstName);
                        command.Parameters.AddWithValue("@LastName", candidate.LastName);
                        command.Parameters.AddWithValue("@PhoneNumber", candidate.PhoneNumber == null ?ExistingPhoneNumber :  candidate.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", candidate.Email);
                        command.Parameters.AddWithValue("@PreferedCallInterval", candidate.PreferedCallInterval == null ? ExistingPreferedCallInterval: candidate.PreferedCallInterval);
                        command.Parameters.AddWithValue("@LinkedInProfile", candidate.LinkedInProfile == null ? ExistingLinkedInProfile : candidate.LinkedInProfile);
                        command.Parameters.AddWithValue("@GitHubProfile", candidate.GitHubProfile == null ? ExistingGitHubProfile : candidate.GitHubProfile);
                        command.Parameters.AddWithValue("@Comment", candidate.Comment);

                       await command.ExecuteNonQueryAsync();
                    }


                    await connection.CloseAsync();
                return new Candidate {
                        FirstName = candidate.FirstName,
                        LastName = candidate.LastName,
                        Email = candidate.Email,
                        PhoneNumber = candidate.PhoneNumber == null ? ExistingPhoneNumber : candidate.PhoneNumber,
                        PreferedCallInterval = candidate.PreferedCallInterval == null ? ExistingPreferedCallInterval : candidate.PreferedCallInterval,
                        GitHubProfile = candidate.GitHubProfile == null ? ExistingGitHubProfile : candidate.GitHubProfile,
                        LinkedInProfile = candidate.LinkedInProfile == null ? ExistingLinkedInProfile : candidate.LinkedInProfile,
                        Comment = candidate.Comment };
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw ex;
                }
            }
        }
    }
}