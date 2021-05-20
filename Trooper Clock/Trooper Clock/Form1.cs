using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Trooper_Clock
{
    public partial class TrooperTime : Form
    {
        ulong troopers;
        ulong squads;
        ulong platoons;
        ulong companies;
        ulong battalions;
        ulong regiments;
        ulong legions;
        ulong deathStars;
        ulong empire;
        ulong displayRegiments;

        private DateTime beginning = new DateTime(2021, 5, 20, 1, 6, 0);
        //private DateTime beginning = new DateTime(2001, 3, 18, 11, 7, 0);
        //private DateTime beginning = new DateTime(1997, 12, 17, 11, 7, 0);

        private int timeTillTick;

        public TrooperTime()
        {
            InitializeComponent();
        }

        private void TrooperUpdate_Tick(object sender, EventArgs e)
        {
            troopers++;
            CalculateUnits();
            UpdateTime();
        }

        private void TrooperTime_Load(object sender, EventArgs e)
        {
            //Get the tim
            //Convert to troopers
            SetTimeStart();
            UpdateTime();
            timeTillTick = 31200 - (int) CalculateTickOffset();

            //Start timer at the right time
            Thread.Sleep(timeTillTick);
            TrooperUpdate.Start();
            troopers++;
            CalculateUnits();
            UpdateTime();
        }

        private void CalculateUnits()
        {
            if(troopers >= 10)
            {
                squads++;
                troopers %= 10;
            }
            if(squads >= 5)
            {
                platoons++;
                squads %= 5;
            }
            if(platoons >= 4)
            {
                companies++;
                platoons %= 4;
            }
            if(companies >= 4)
            {
                battalions++;
                companies %= 4;
            }
            if(battalions >= 4)
            {
                regiments++;
                displayRegiments++;
                battalions %= 4;
            }
            if(regiments >= 4)
            {
                legions++;
                regiments %= 4;
            }
            if(displayRegiments >= 10)
            {
                deathStars++;
            }
            if(deathStars >= 9)
            {
                //Error probably here
                empire++;
                deathStars %= 9;
                legions = 0;
                regiments = 0;
                displayRegiments = 0;
                battalions = 0;
                companies = 0;
                platoons = 0;
                squads = 0;
                troopers = 0;
            }
        }
        private void UpdateTime()
        {
            BCPST_Data.Text = battalions.ToString() + companies.ToString() + platoons.ToString() + squads.ToString() + troopers.ToString();
            RegimentData.Text = displayRegiments.ToString();
            LegionData.Text = legions.ToString();
            DeathStarData.Text = deathStars.ToString();
            EmpireData.Text = empire.ToString();
        }

        private ulong CalculateTickOffset()
        {
            DateTime now = DateTime.UtcNow;
            TimeSpan timePassed = now - beginning;
            ulong timePassedMil = (ulong)timePassed.TotalMilliseconds;
            ulong trooperOffset = timePassedMil % 31200;

            return trooperOffset;
        }

        private ulong CalculateStartTroopers()
        {
            DateTime now = DateTime.UtcNow;
            TimeSpan timePassed = now - beginning;
            ulong timePassedMil = (ulong)timePassed.TotalMilliseconds;
            ulong totalTroopers = timePassedMil / 31200;

            return totalTroopers;
        }
        private void SetTimeStart()
        {
            troopers = CalculateStartTroopers();

            if (troopers >= 10)
            {
                squads = troopers/10;
                troopers %= 10;
            }
            if (squads >= 5)
            {
                platoons = squads/5;
                squads %= 5;
            }
            if (platoons >= 4)
            {
                companies = platoons/4;
                platoons %= 4;
            }
            if (companies >= 4)
            {
                battalions = companies/4;
                companies %= 4;
            }
            if (battalions >= 4)
            {
                regiments = battalions/4;
                displayRegiments = battalions / 4;
                battalions %= 4;
            }
            if (regiments >= 4)
            {
                legions = regiments/4;
                regiments %= 4;
            }
            if (displayRegiments >= 10)
            {
                deathStars = displayRegiments/10;
                displayRegiments %= 10;
                legions = displayRegiments / 4;
            }
            if (deathStars >= 9)
            {
                empire = deathStars / 9;
                deathStars %= 9;
            }
        }

    }
}
