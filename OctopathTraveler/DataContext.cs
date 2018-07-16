﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace OctopathTraveler
{
	class DataContext
	{
		public ObservableCollection<Charactor> Charactors { get; set; } = new ObservableCollection<Charactor>();
		public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
		public List<NameValueInfo> Jobs { get; private set; } = Info.Instance().Jobs;

		private readonly uint mMoneyAddress;
		public DataContext()
		{
			SaveData save = SaveData.Instance();
			foreach (var address in save.FindAddress("CharacterID_", 0))
			{
				var chara = new Charactor(address);
				if (chara.ID < 0 || chara.ID > 8) continue;
				Charactors.Add(chara);
			}

			foreach (var address in save.FindAddress("ItemID_", 0))
			{
				Items.Add(new Item(address));
			}

				mMoneyAddress = save.FindAddress("Money", 0)[0] + 0x42;
		}

		public uint Money
		{
			get { return SaveData.Instance().ReadNumber(mMoneyAddress, 4); }
			set { Util.WriteNumber(mMoneyAddress, 4, value, 0, 9999999); }
		}
	}
}
