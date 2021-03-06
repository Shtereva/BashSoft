﻿namespace BashSoft.Contracts.Repo.Database
{
    public interface IDatabase : IRequester, IFilteredTaker, IOrderedTaker
    {
        void LoadData(string fileName);
        void UnloadData();
    }
}
