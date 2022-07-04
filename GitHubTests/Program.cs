using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubTests
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));

            var tokenAuth = new Credentials("jkdfg_FSShs4S543hdgbds4352fgdgfdg3423dsd"); // not real token
            client.Credentials = tokenAuth;

            var user = await client.User.Get("iceval");
            Console.WriteLine("{0} has {1} public repositories - go check out their profile at {2}",
                user.Name,
                user.PublicRepos,
                user.Url);

            var projects = await client
                .Repository
                .Project
                .GetAllForRepository("iceval", "DevelopmentSoft.Timesheet");

            var timesheetProject = projects.FirstOrDefault();

            var columns = await client.Repository.Project.Column
                .GetAll(timesheetProject.Id);

            var cards = new List<ProjectCard>();

            foreach (var column in columns)
            {
                var columnCards = await client.Repository.Project.Card
                    .GetAll(column.Id);

                cards.AddRange(columnCards);
            }
        }
    }
}
