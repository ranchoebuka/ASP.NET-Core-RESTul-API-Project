﻿using EuropeLeagues.API.DbContexts;
using EuropeLeagues.API.Entities;
using EuropeLeagues.API.SearchUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuropeLeagues.API.Repository
{
    public class EuropeanLeaguesRepository : IEuropeanLeaguesRepository
    {
        private readonly EuropeLeagueContext _context;
        public EuropeanLeaguesRepository(EuropeLeagueContext europeLeagueContext)
        {
            _context = europeLeagueContext;
        }
        public FootballClub GetClub(int LeagueId, int ClubId)
        {
            if (LeagueId == 0)
            {
                throw new WrongIDException("Id Cannot be Zero");
            }
            if (ClubId == 0)
            {
                throw new WrongIDException("Id Cannot be Zero");
            }

            var club =  _context.Clubs.ToList<FootballClub>().Where(x => x.LeagueId == LeagueId && x.Id == ClubId).FirstOrDefault();
            if (club!=null)
            {
                club.League = _context.Leagues.ToList<League>().Find(x => x.Id == LeagueId);
            }
           

            return club;
        }

        public IEnumerable<FootballClub> GetClubs(int LeagueId, FootballClubSearchCriteria searchcriteria)
        {
            IEnumerable<FootballClub> clubs = null;
            if (LeagueId == 0)
            {
                throw new WrongIDException("Id Cannot be Zero");
            }
            clubs = searchcriteria == null ? 
                _context.Clubs.ToList<FootballClub>().OrderBy(c => c.Id).Where(x => x.LeagueId == LeagueId):
                GetClubsWithEqualorGreaterThanCapacity(LeagueId, searchcriteria.capacity);

            var league = _context.Leagues.ToList<League>().Find(x => x.Id == LeagueId);

            foreach (var item in clubs)
            {
                item.League = league;
            }

            return clubs;
        }

        public IEnumerable<FootballClub> GetClubsWithEqualorGreaterThanCapacity(int LeagueId, double capacity)
        {
            if (LeagueId == 0)
            {
                throw new WrongIDException("Id Cannot be Zero");
            }
            var clubs = _context.Clubs.ToList<FootballClub>().Where(x => x.LeagueId == LeagueId && x.stadiumCapacity >= capacity);

            var league = _context.Leagues.ToList<League>().Find(x => x.Id == LeagueId);

            foreach (var item in clubs)
            {
                item.League = league;
            }

            return clubs;
        }

        public League GetLeague(int LeagueId)
        {
            if (LeagueId == 0)
            {
                throw new WrongIDException("Id Cannot be Zero");
            }
            return _context.Leagues.ToList<League>().FirstOrDefault(x => x.Id == LeagueId);
        }

        public IEnumerable<League> GetLeagues(LeagueSearchCriteria searchcriteria)
        {
            if (searchcriteria == null)
            {
                return _context.Leagues.ToList<League>();
            }
            else
            {
                return GetLeagues(searchcriteria.leagueGroup.ToLower());
            }
        }

        public IEnumerable<League> GetLeagues(string group)
        {
           
            return _context.Leagues.ToList<League>().Where(x=>x.Group.ToLower() == group);
        }

        public bool LeagueExist(int leagueId)
        {
            if (leagueId == 0)
            {
                throw new WrongIDException("Id Cannot be Zero");
            }

            return _context.Leagues.Any(x => x.Id == leagueId);
        }
    }
}
