using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;

public class TeamRepository
{
    private readonly List<User> _users = new List<User>(); // Simulated database for users
    private readonly List<Team> _teams = new List<Team>(); // Simulated database for teams
    private readonly List<Player> _players = new List<Player>(); // Simulated database for players
    public Team CreateTeam(NewTeamViewModel newTeam)
    {
        _users.Add(new User { Id = 2, Nickname = "TeamCoach" });
        // Simulate finding the team coach user
        var teamCoachUser = _users.Find(u => u.Nickname == newTeam.TeamCoachNickName);
        if (teamCoachUser == null)
        {
            throw new InvalidOperationException("Team coach user not found.");
        }

        // Simulate creating the team
        var team = new Team
        {
            Id = 1,
            TeamName = newTeam.TeamName,
            PlayerCount = newTeam.PlayerCount,
            TeamCoach = teamCoachUser.Id
        };

        // Simulate adding team coach to team coach list
        var teamCoach = new TeamCoach { UserId = teamCoachUser.Id, NickName = teamCoachUser.Nickname, TeamName = team.TeamName };

        // Simulate adding players to the team
        for (int i = 0; i < team.PlayerCount; i++)
        {
            var player = new Player { TeamId = team.Id };
            _players.Add(player);
        }

        // Simulate saving the team
        _teams.Add(team);
        return team;
    }

    public BaseResponse<string> DeleteTeam(long teamId)
    {
        // Simulate finding and removing the team
        var team = _teams.Find(t => t.Id == teamId);
        if (team == null)
        {
            return new BaseResponse<string>
            {
                StatusCode = 547,
                Success = false,
                Message = "Team not found.",
                Data = "Error: Team not found."
            };
        }

        // Simulate removing the team
        _teams.Remove(team);
        return new BaseResponse<string>
        {
            StatusCode = 200,
            Success = true,
            Message = "Team deleted.",
            Data = "Error: None"
        };
    }

    public Team SaveTeam(TeamViewModel teamModel)
    {
        _users.Add(new User { Id = 2, Nickname = "TeamCoach" });

        // Simulate finding the team
        var team = _teams.Find(t => t.Id == teamModel.Id);
        if (team == null)
        {
            throw new InvalidOperationException("Team not found.");
        }

        // Simulate updating team information
        team.TeamSite = teamModel.TeamSite;
        team.Desc = teamModel.Desc;

        // Simulate updating team coach
        var teamCoachUser = _users.Find(u => u.Nickname == teamModel.TeamCoach.NickName);
        if (teamCoachUser != null)
        {
            team.TeamCoach = teamCoachUser.Id;
        }

        // Simulate updating player information
        for (int i = 0; i < team.PlayerCount; i++)
        {
            var player = _players.Find(p => p.Id == teamModel.Players[i].Id);
            if (player != null && teamModel.Players[i].NickName != null)
            {
                player.NickName = teamModel.Players[i].NickName;
                player.UserId = teamModel.Players[i].UserId;
            }
        }

        return team;
    }
}
namespace SocialNetworkProject.Tests.RepositoryTests
{
    [TestClass]
    public class TeamRepositoryTests
    {
        [TestMethod]
        public void CreateTeam_ValidInput_Success()
        {
            // Arrange
            var repository = new TeamRepository();
            var newTeam = new NewTeamViewModel
            {
                TeamName = "Team1",
                PlayerCount = 5,
                TeamCoachNickName = "TeamCoach",
               
            };
            var newTeamCoach = new TeamCoach
            {
                NickName = "TeamCoach",
                Id = 2
            };

            // Act
            var result = repository.CreateTeam(newTeam);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newTeam.TeamName, result.TeamName);
            Assert.AreEqual(newTeam.PlayerCount, result.PlayerCount);
            Assert.AreEqual(newTeamCoach.Id, result.TeamCoach);
        }

        [TestMethod]
        public void DeleteTeam_ValidTeamId_Success()
        {
            // Arrange
            var repository = new TeamRepository();
            var teamId = 1;
            var newTeam = new NewTeamViewModel { TeamName = "Team1", PlayerCount = 5, TeamCoachNickName = "TeamCoach" };
            repository.CreateTeam(newTeam);

            // Act
            var result = repository.DeleteTeam(teamId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Team deleted.", result.Message);
        }

        [TestMethod]
        public void DeleteTeam_InvalidTeamId_Failure()
        {
            // Arrange
            var repository = new TeamRepository();
            var teamId = 1;

            // Act
            var result = repository.DeleteTeam(teamId);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Team not found.", result.Message);
        }

        [TestMethod]
        public void SaveTeam_ValidInput_Success()
        {
            // Arrange
            var repository = new TeamRepository();
            var teamId = 1;

            var newTeam = new NewTeamViewModel { TeamName = "Team1", PlayerCount = 3, TeamCoachNickName = "TeamCoach" };
            var team = repository.CreateTeam(newTeam);

            var teamModel = new TeamViewModel
            {
                Id = teamId,
                TeamSite = "http://example.com",
                Desc = "Description",
                TeamCoach = new TeamCoach {Id = 2, NickName = "TeamCoach" },
                Players = new List<Player> { new Player { Id = 1, NickName = "Player1", UserId = 1 },
                                             new Player { Id = 2, NickName = "Player1", UserId = 2 },
                                             new Player { Id = 3, NickName = "Player1", UserId = 3 }},
                PlayerCount = team.PlayerCount
            };

            // Act
            var result = repository.SaveTeam(teamModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(teamModel.TeamSite, result.TeamSite);
            Assert.AreEqual(teamModel.Desc, result.Desc);
            Assert.AreEqual(teamModel.TeamCoach.Id, result.TeamCoach);
            Assert.AreEqual(teamModel.Players.Count, result.PlayerCount);
        }
    }

}