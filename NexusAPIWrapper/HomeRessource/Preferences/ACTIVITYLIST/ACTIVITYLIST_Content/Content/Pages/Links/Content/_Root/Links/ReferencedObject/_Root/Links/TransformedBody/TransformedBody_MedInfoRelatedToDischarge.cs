namespace NexusAPIWrapper.HomeRessource.Preferences.ACTIVITYLIST.ACTIVITYLIST_Content.Content.Pages.Links.Content._Root.Links.ReferencedObject._Root.Links.TransformedBody
{
    public class TransformedBody_MedInfoRelatedToDischarge
    {
        /// <summary>
        /// Feltet "Medsendt medicin"
        /// </summary>
        public string EnclosedMedicationDate { get; set; }
        /// <summary>
        /// Feltet "Kommentar til medsent medicin"
        /// </summary>
        public string CommentsForEnclosedMedication {  get; set; }
        /// <summary>
        /// Feltet "Recept til apotek"
        /// </summary>
        public bool PrescriptionForPharmacy { get; set; }
        /// <summary>
        /// Feltet "Afhentning/Udbringning aftalt"
        /// </summary>
        public bool PickupDeliveryAgreed { get; set; }
        /// <summary>
        /// Feltet "Dosisdispensering genbestilt"
        /// </summary>
        public bool DoseDispensingReordered { get; set; }
    }
}