using System;
using System.Collections.Generic;

namespace PatchaWallet.Wallet
{
    public static class Factory
    {
        public static SimulateGoalDocument ToDocument(this SimulateGoalVM simulateGoalVM)
        {
            var document = new SimulateGoalDocument()
            {
                Id = simulateGoalVM.Id,
                AnnualPercente = simulateGoalVM.AnnualPercente,
                BeginValue = simulateGoalVM.BeginValue,
                DateKind = simulateGoalVM.DateKind,
                Contributions = simulateGoalVM.Contributions?.ToDocument()
            };
            return document;
        }

        public static List<ContributionDocument> ToDocument(this List<ContributionVM> contributions)
        {
            List<ContributionDocument> documents = new List<ContributionDocument>();
            foreach (var item in contributions)
            {
                documents.Add(item.ToDocument());
            }
            return documents;
        }

        public static ContributionDocument ToDocument(this ContributionVM contributionVM)
        {
            var document = new ContributionDocument()
            {
                Id = contributionVM.Id,
                Value = contributionVM.Value,
                Date = contributionVM.Date
            };
            return document;
        }

    }
}
