using System;
using System.Collections.Generic;

namespace PatchaWallet.Wallet
{
    public static class Factory
    {
        public static SimulateGoalDocument ToDocument(this SimulateGoalVM simulateGoalVM)
        {
            if (simulateGoalVM == null) return null;

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

        public static SimulateGoalVM ToVM(this SimulateGoalDocument document)
        {
            if (document == null) return null;

            var vm = new SimulateGoalVM()
            {
                Id = document.Id,
                AnnualPercente = document.AnnualPercente,
                BeginValue = document.BeginValue,
                DateKind = document.DateKind,
                Contributions = document.Contributions?.ToVM()
            };
            return vm;
        }

        public static List<ContributionDocument> ToDocument(this List<ContributionVM> contributions)
        {
            if (contributions == null) return null;

            List<ContributionDocument> documents = new List<ContributionDocument>();
            foreach (var item in contributions)
            {
                documents.Add(item.ToDocument());
            }
            return documents;
        }

        public static List<ContributionVM> ToVM(this List<ContributionDocument> contributions)
        {
            if (contributions == null) return null;

            List<ContributionVM> vms = new List<ContributionVM>();
            foreach (var item in contributions)
            {
                vms.Add(item.ToVM());
            }
            return vms;
        }

        public static ContributionDocument ToDocument(this ContributionVM contributionVM)
        {
            if (contributionVM == null) return null;

            var document = new ContributionDocument()
            {
                Id = contributionVM.Id,
                Value = contributionVM.Value,
                Date = contributionVM.Date
            };
            return document;
        }

        public static ContributionVM ToVM(this ContributionDocument document)
        {
            if (document == null) return null;

            var vm = new ContributionVM()
            {
                Id = document.Id,
                Value = document.Value,
                Date = document.Date
            };
            return vm;
        }

    }
}
