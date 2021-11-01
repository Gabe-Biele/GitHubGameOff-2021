using BieleStudios.Shared.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BieleStudios.GitHubGameOff.Data
{
    public static class Projectiles
    {
        private static DataDictionary<Projectile, ProjectileData> projectiles;

        private static void PopulateProjectiles()
        {
            projectiles = new DataDictionary<Projectile, ProjectileData>(data => data.Id);
            projectiles.Add(new ProjectileData(Projectile.Arrow, "Arrow", "Projectiles/Arrow", 10, 7));
            //projectiles.Add(new ProjectileData(Projectile.ElectroFireball, "Electro Fireball", "Effects/ElectroFireball", 10, 10));
        }

        public static ProjectileData Get(Projectile id)
        {
            if (projectiles is null) PopulateProjectiles();
            return projectiles[id];
        }
    }

    public class ProjectileData
    {
        public Projectile Id;
        public string Name;
        public string ResourceLocation;

        public float Damage;
        public float DefaultSpeed;

        public ProjectileData(Projectile id, string name, string resource, float dmg, float defaultSpd)
        {
            Id = id;
            Name = name;
            ResourceLocation = resource;
            Damage = dmg;
            DefaultSpeed = defaultSpd;
        }
    }

    public enum Projectile { Arrow }
}
