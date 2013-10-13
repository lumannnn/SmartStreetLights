using System;
using UnityEngine;

namespace SmartStreetLights.State
{
	public interface State {
		void On(StreetLight light);
		void Off(StreetLight light);
		void DimUp(StreetLight light, float step);
		void DimDown(StreetLight light, float step);
	}
}