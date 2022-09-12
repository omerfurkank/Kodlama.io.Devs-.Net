using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SocialLink : Entity
    {
        public int UserId { get; set; }
        public string GithubAdress { get; set; }
        public User? User { get; set; }
        public SocialLink()
        {

        }

        public SocialLink(int id,int userId, string githubAdress) : this()
        {
            Id = id;
            UserId = userId;
            GithubAdress = githubAdress;
        }
    }
}
