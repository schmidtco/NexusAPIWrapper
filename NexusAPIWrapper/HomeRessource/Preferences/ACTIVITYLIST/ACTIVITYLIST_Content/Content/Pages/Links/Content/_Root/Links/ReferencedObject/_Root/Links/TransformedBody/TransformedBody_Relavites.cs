using NUnit.Framework;
using System.Collections.Generic;

namespace NexusAPIWrapper.HomeRessource.Preferences.ACTIVITYLIST.ACTIVITYLIST_Content.Content.Pages.Links.Content._Root.Links.ReferencedObject._Root.Links.TransformedBody
{
    public class TransformedBody_Relavites
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<TransformedBody_ContactInformation> ContactInformation { get; set; }
        public bool IsInformed { get; set; }


        public TransformedBody_Relavites()
        {
            ContactInformation = new List<TransformedBody_ContactInformation>();
        }
    }
}