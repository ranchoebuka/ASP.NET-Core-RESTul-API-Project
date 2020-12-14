﻿using EuropeLeagues.API.Entities;
using EuropeLeagues.API.SearchUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuropeLeagues.API.Repository
{
    public interface IEuropeanLeaguesRepository
    {
        IEnumerable<FootballClub> GetClubs(int LeagueId, FootballClubSearchCriteria searchcriteria);
        FootballClub GetClub(int LeagueId, int ClubId);
        IEnumerable<League> GetLeagues(LeagueSearchCriteria searchcriteria);
        IEnumerable<FootballClub> GetClubsWithEqualorGreaterThanCapacity(int LeagueId, double capacity);
        League GetLeague(int LeagueId);
        bool LeagueExist(int leagueId);
    }
}
