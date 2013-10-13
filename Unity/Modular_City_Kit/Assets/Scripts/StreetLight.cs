﻿using UnityEngine;
using System.Collections;

using SmartStreetLights.State;

public class StreetLight : MonoBehaviour {
	
	private int _id;
	private float _maxIntensity;
	
	private State _state;
	
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
		SetState(new OffState());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public Light GetLight() {
		return light;
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
}