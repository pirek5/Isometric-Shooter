using System;

namespace IsoShooter.Weapons
{
    public interface IReloadableWeapon
    {
        public event Action OnMagazineStatusChanged;

        public MagazineStatus GetStatus();
    }
    
    public struct MagazineStatus
    {
        public bool IsReloading;
        /// <summary>
        /// value between 0 and 1, 0 means reload just started, 1 reload finished 
        /// </summary>
        public float ReloadStatus;
        public int ProjectilesLeftInMagazine;
        public int MagazineCapacity;
    }
}





