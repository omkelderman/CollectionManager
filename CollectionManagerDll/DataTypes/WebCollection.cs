﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionManager.Modules.FileIO.OsuDb;

namespace CollectionManager.DataTypes
{
    public class WebCollection : Collection
    {
        public bool Loading { get; private set; }
        public bool Loaded { get; private set; }
        public WebCollection(int onlineId, MapCacher instance) : base(instance)
        {
            OnlineId = onlineId;
        }
        /// <summary>
        /// Fetches this collection data from internet
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public async Task Load(IWebCollectionProvider provider)
        {
            if (Loading || Loaded || !provider.CanFetch())
                return;

            Loading = true;

            var collection = await provider.GetCollection(OnlineId);

            foreach (var b in collection.AllBeatmaps())
            {
                AddBeatmap(b);
            }

            Loaded = true;
            Loading = false;
        }

        public async Task Save(IWebCollectionProvider provider)
        {
            await provider.SaveCollection(this);
        }

        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Title
        {
            get => Name;
            set => Name = value;
        }

        public int Id
        {
            get => OnlineId;
            set => OnlineId = value;
        }

        private int _numberOfBeatmaps;
        public override int NumberOfBeatmaps
        {
            get
            {
                if (this.Loaded)
                {
                    return base.NumberOfBeatmaps;
                }

                return _numberOfBeatmaps;
            }
            set => _numberOfBeatmaps = value;
        }

        protected override void ProcessNewlyAddedMap(BeatmapExtension map)
        {
            if (Loading || Loaded)
            {
                base.ProcessNewlyAddedMap(map);
            }
        }
    }

    public interface IWebCollectionProvider
    {
        Task<ICollection> GetCollection(int collectionId);
        Task<IEnumerable<WebCollection>> SaveCollection(ICollection collection);
        bool CanFetch();
    }
}