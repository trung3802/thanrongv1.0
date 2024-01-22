using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Bosses
{
    public class Clone
    {
        public IList<TienKiemV2Remastered.Application.Threading.Map> CloneMap { get; set; }
        public Clone()
        {
            CloneMap = new List<TienKiemV2Remastered.Application.Threading.Map>();
            CloneMap.Clear();
            CloneMap.Add(new TienKiemV2Remastered.Application.Threading.Map(139, tileMap: null, mapCustom: null));
            CloneMap.Add(new TienKiemV2Remastered.Application.Threading.Map(140, tileMap: null, mapCustom: null));
        }
        public void Start(TienKiemV2Remastered.Model.Character.Character character)
        {
            var boss = new Boss();
            boss.CreateBossClone(character, character.HpFull, character.MpFull, character.DamageFull, character.DefenceFull);
            boss.CharacterHandler.SetUpInfo();
            character.Clone.CloneMap[1].Zones[0].ZoneHandler.AddBoss(boss);
        }
        public void Remove(Character character, int index)
        {
            CloneMap.Remove(CloneMap[index]);
        }
        public void RemoveById(int idmap)
        {
            CloneMap.Remove(CloneMap.FirstOrDefault(i => i.TileMap.Id == idmap));
        }
        public void Add(Character character,int index ,int idMap)
        {
            CloneMap[index] = new TienKiemV2Remastered.Application.Threading.Map(idMap, tileMap: null, mapCustom: null);
        }
        public void Clear()
        {
            CloneMap.Clear();
        }
    }
}
