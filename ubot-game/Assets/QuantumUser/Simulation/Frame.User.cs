using System.Diagnostics.Tracing;

namespace Quantum
{
    public unsafe partial class Frame
    {

		public Config Config
		{
			get
			{
				return this.FindAsset<Config>(this.RuntimeConfig.Config.Id);
			}
		}

		public EntityRef GetSubmarine(TeamRef team)
		{
			var filter = this.Filter<Submarine, TeamLink>();
			while (filter.Next(out var entity, out var submarine, out var teamLink))
			{
				if (teamLink.Team == team)
				{
					return entity;
				}
			}
			return EntityRef.None;
		}


#if UNITY_ENGINE

#endif
	}
}