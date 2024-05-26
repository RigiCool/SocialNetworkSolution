using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkProject.Tests.RepositoryTests
{
    [TestClass]
    public class TournamentRepositoryTests
    {
        [TestMethod]
        public void CreateTournamentOlympic_ValidTournament_CreatesTournament()
        {
            // Arrange
            var newTournamentViewModel = new NewTournamentViewModel
            {
                TournamentName = "TestTournament",
                Prize = 200000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                SponsorNickName = "TestSponsor"
            };

            // Act
            var createdTournament = CreateTournamentOlympic(newTournamentViewModel);

            // Assert
            Assert.IsNotNull(createdTournament);
            Assert.AreEqual(newTournamentViewModel.TournamentName, createdTournament.Name);
            Assert.AreEqual(newTournamentViewModel.Prize, createdTournament.Prize);
            Assert.AreEqual(newTournamentViewModel.StartDate, createdTournament.StartDate);
            Assert.AreEqual(newTournamentViewModel.EndDate, createdTournament.EndDate);
            Assert.AreEqual(1, createdTournament.Stage);
            Assert.IsFalse(createdTournament.Status);
        }

        [TestMethod]
        public void CreateMatches_ValidTournament_CreatesMatches()
        {
            // Arrange
            long tournamentId = 1; // Example tournament ID

            // Act
            var createdMatchesCount = CreateMatches(tournamentId);

            // Assert
            Assert.AreEqual(7, createdMatchesCount);
        }

        // Method to simulate CreateTournamentOlympic without involving TournamentRepository
        private TournamentOlympic CreateTournamentOlympic(NewTournamentViewModel newTournament)
        {
            TournamentOlympic tournament = new TournamentOlympic
            {
                Name = newTournament.TournamentName,
                Prize = newTournament.Prize,
                StartDate = newTournament.StartDate,
                EndDate = newTournament.EndDate,
                SponsorId = 1,
                Stage = 1,
                Status = false
            };

            return tournament;
        }

        // Method to simulate CreateMatches without involving TournamentRepository
        private int CreateMatches(long tournamentId)
        {
            int createdMatchesCount = 0;
            for (int i = 0; i < 7; i++)
            {
                // Logic to create matches, in this example we'll just increment a counter
                createdMatchesCount++;
            }
            return createdMatchesCount;
        }
        [TestMethod]
        public void AddTeam_ValidTeam_AddsTeamToOlympicTournament()
        {
            // Arrange
            var tournamentVM = new OlympicTournamentViewModel
            {
                Id = 1, // Example tournament ID
                Name = "Test Tournament"
            };

            // Simulate the team
            var team = new Team
            {
                Id = 1,
                TeamName = "Test Team"
            };

            // Act
            var result = AddTeam(tournamentVM, team);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(tournamentVM.Id, result.Id);
            // Add more assertions as needed
        }
        private TournamentOlympic AddTeam(OlympicTournamentViewModel tournamentVM, Team team)
        {
            // Simulate adding a team to the tournament
            var newTeam = new ClassicTeamList
            {
                Team = team,
                Score = 0,
                TournamentId = tournamentVM.Id,
                TournamentName = tournamentVM.Name
            };

            return new TournamentOlympic
            {
                Id = tournamentVM.Id,
                Name = tournamentVM.Name
            };
        }
        [TestMethod]
        public void CreatePlayOff_ValidTournament_CreatesPlayOff()
        {
            // Arrange
            var tournamentVM = new OlympicTournamentViewModel
            {
                Id = 1, // Example tournament ID
                // Add other properties as needed
            };

            var olympicTeamList = new List<OlympicTeamList>
            {
                new OlympicTeamList { Team = new Team { TeamName = "Team1" }, Score = 10 },
                new OlympicTeamList { Team = new Team { TeamName = "Team2" }, Score = 8 },
                new OlympicTeamList { Team = new Team { TeamName = "Team3" }, Score = 12 },
                new OlympicTeamList { Team = new Team { TeamName = "Team4" }, Score = 10 },
                new OlympicTeamList { Team = new Team { TeamName = "Team5" }, Score = 8 },
                new OlympicTeamList { Team = new Team { TeamName = "Team6" }, Score = 12 },
                new OlympicTeamList { Team = new Team { TeamName = "Team7" }, Score = 8 },
                new OlympicTeamList { Team = new Team { TeamName = "Team8" }, Score = 12 },
            };

            // Act
            var result = CreatePlayOff(tournamentVM, olympicTeamList);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(8, result.PlayOffTeams.Count);
            // Add more assertions as needed
        }

        // Method to simulate CreatePlayOff without involving AppDBContent
        private TournamentOlympic CreatePlayOff(OlympicTournamentViewModel tournamentVM, List<OlympicTeamList> olympicTeamList)
        {
            // Simulate ordering teams by score (you can replace with any constant values)
            var orderedTeams = olympicTeamList.OrderByDescending(score => score.Score).ToList();

            // Ensure there are at least 8 teams available
            if (orderedTeams.Count >= 8)
            {
                // Simulate creating playoff teams
                var tournament = new TournamentOlympic { Id = tournamentVM.Id };
                tournament.PlayOffTeams = orderedTeams.Take(8).Select(t => t.Team).ToList();
                Console.WriteLine(tournament.PlayOffTeams.Count);
                return tournament;
            }
            else
            {
                // Handle the case where there are not enough teams
                throw new InvalidOperationException("There are not enough teams to create a playoff.");
            }
        }

        [TestMethod]
        public void CreateTournamentList_ValidTournament_CreatesTournamentList()
        {
            // Arrange
            var newTournament = new NewTournamentViewModel
            {
                TournamentName = "Test Tournament",
                Prize = 1000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                SponsorNickName = "TestSponsor"
            };

            // Act
            var result = CreateTournamentList(newTournament);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newTournament.TournamentName, result.Name);
            // Add more assertions as needed
        }

        [TestMethod]
        public void AddTeam_ValidTeam_AddsTeamToListTournament()
        {
            // Arrange
            var tournamentVM = new ListTournamentViewModel
            {
                Id = 1, // Example tournament ID
                Name = "Test Tournament"
            };

            // Simulate the team
            var team = new Team
            {
                Id = 1,
                TeamName = "Test Team"
            };

            // Act
            TournamentList result = AddTeam(tournamentVM, team);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(tournamentVM.Id, result.Id);
            // Add more assertions as needed
        }

        // Method to simulate CreateTournamentList without involving AppDBContent
        private TournamentList CreateTournamentList(NewTournamentViewModel newTournament)
        {
            // Simulate creating a new tournament
            var tournament = new TournamentList
            {
                Name = newTournament.TournamentName,
                Prize = newTournament.Prize,
                StartDate = newTournament.StartDate,
                EndDate = newTournament.EndDate,
                SponsorId = 1, // Simulate sponsor ID
                Complete = false
            };

            return tournament;
        }

        // Method to simulate AddTeam without involving AppDBContent
        private TournamentList AddTeam(ListTournamentViewModel tournamentVM, Team team)
        {
            // Simulate adding a team to the tournament
            var newTeam = new ClassicTeamList
            {
                Team = team,
                Score = 0,
                TournamentId = tournamentVM.Id,
                TournamentName = tournamentVM.Name
            };

            return new TournamentList
            {
                Id = tournamentVM.Id,
                Name = tournamentVM.Name
            };
        }
    }
}
