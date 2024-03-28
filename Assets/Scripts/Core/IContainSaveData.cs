using Assets.Scripts.Dto;

namespace Assets.Scripts.Core
{
    public interface IContainSaveData<T> where T : SaveData
    {
        public void SaveData(T data);

        public void LoadData();
    }
}
