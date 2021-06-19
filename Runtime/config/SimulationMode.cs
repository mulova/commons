//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

namespace mulova.unicore
{
	public enum SimulationMode {
		ReleaseBuild, DebugBuild, Editor
	}
	
	public static class SimulationModeEx {
		public static bool IsBuild(this SimulationMode mode) {
			return mode == SimulationMode.ReleaseBuild || mode == SimulationMode.DebugBuild;
		}
	}
}