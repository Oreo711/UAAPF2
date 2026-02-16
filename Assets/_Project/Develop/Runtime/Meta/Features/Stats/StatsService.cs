using _Project.Develop.Runtime.Utilities.DataManagement;
using _Project.Develop.Runtime.Utilities.DataManagement.DataProviders;


namespace _Project.Develop.Runtime.Meta.Features.Stats
{
	public class StatsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
	{
		private int _wins;
		private int _losses;

		public StatsService (PlayerDataProvider playerDataProvider)
		{
			playerDataProvider.RegisterWriter(this);
			playerDataProvider.RegisterReader(this);
		}

		public int Wins      => _wins;
		public int Losses    => _losses;
		public int ResetCost {get;} = 50;

		public void IncrementWins ()
		{
			_wins++;
		}

		public void IncrementLosses ()
		{
			_losses++;
		}

		public void Reset ()
		{
			_wins   = 0;
			_losses = 0;
		}

		public void ReadFrom (PlayerData data)
		{
			_wins   = data.Wins;
			_losses = data.Losses;
		}

		public void WriteTo (PlayerData data)
		{
			data.Wins   = _wins;
			data.Losses = _losses;
		}
	}
}
