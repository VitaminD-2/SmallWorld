﻿using Assets.Model;
using System;
using System.Collections.Generic;

namespace Assets.Repo
{
    public class CardRepo
    {
        public PowerRepo powerRepo;
        public RaceRepo raceRepo;

        public CardRepo()
        {
            powerRepo = new PowerRepo();
            raceRepo = new RaceRepo();
        }

        public List<Card> GetCards(int count)
        {
            var powers = powerRepo.GetPowers(count);
            var races = raceRepo.GetRaces(count);
            var cards = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                cards.Add(new Card 
                { 
                    Id = Guid.NewGuid().ToString(),
                    Power = powers[i], 
                    Race = races[i],
                    Claimed = false,
                    VictoryCoinsPlaced = 0
                });
            }

            return cards;
        }

        public List<Power> Test(int count)
        {
            return powerRepo.GetPowers(count);
        }
    }
}
