﻿using UnityEngine;
using System;
using System.Collections;


namespace SmartStreetLights.Lights {
	
	using SmartStreetLights.State;
	
	public class StreetLight : MonoBehaviour {
		
		private int _id;
		private float _maxIntensity;
		
		private StreetPointLight _pointLight;
		
		private State _state;
		
		private long _onTime; // in ms
		private bool _counterStarted;
		private long _startTime; // in ms
		
		public int GetId() {
			return _id;
		}
		
		public void SetId(int id) {
			_id = id;
		}
		
		public void SetMaxIntensity(float max) {
			_maxIntensity = max;
		}
		public float GetMaxIntensity() {
			return _maxIntensity;
		}
		
		// Use this for initialization
		void Start () {
			light.color = Color.white;
			light.intensity = 0;
			
			_pointLight = (StreetPointLight) GetComponentInChildren(typeof(StreetPointLight));
			_pointLight.GetLight().color = Color.white;
			_pointLight.GetLight().intensity = 0;
			
			SetState(new OffState(this));
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		public Light GetLight() {
			return light;
		}
		
		public Light GetPointLight() {
			return _pointLight.GetLight();
		}
		
		public void ToggleOnOff() {
			if (light.intensity >= _maxIntensity) {
				Off();
			} else {
				DimUp(0.1f);
			}
		}
		
		public void On() {
			_state.On(this);
		}
	
		public void Off() {
			_state.Off(this);
		}
	
		public void DimUp(float step) {
			_state.DimUp(this, step);
		}
		
		public void DimDown(float step) {
			_state.DimDown(this, step);
		}
		
		public void SetState(State state) {
			Debug.Log("SSL: " + _id + ", SetState: " + state.GetType());
			_state = state;
		}
		
		public bool IsRunning() {
			if (_state.GetType() == typeof(DimmingUpState) || _state.GetType() == typeof(DimmingDownState)) {
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Increases the on time.
		/// </summary>
		/// <param name='increaseBy'>
		/// Increase by. in ms
		/// </param>
		public void increaseOnTime(long increaseBy) {
			_onTime += increaseBy;
		}
		
		public long getOnTime() {
			if (_counterStarted) {
				return currentTime() - _startTime + _onTime;
			}
			return _onTime;
		}
		
		public void startCounter() {
			if (!_counterStarted) {
				_counterStarted = true;
				_startTime = currentTime();
			}
		}
		
		public void stopCounter() {
			if (_counterStarted) {
				_counterStarted = false;
				_onTime += currentTime() - _startTime;
			}
		}
		
		private long currentTime() {
			return (long) Convert.ToInt64((Time.time * 1000));
		}
		
	}
	
}