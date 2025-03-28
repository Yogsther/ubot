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

#if UNITY_ENGINE

#endif
	}
}