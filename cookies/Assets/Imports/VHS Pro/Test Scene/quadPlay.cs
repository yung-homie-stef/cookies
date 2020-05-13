using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class quadPlay : MonoBehaviour {

	#if !(UNITY_PS4 || UNITY_IOS || UNITY_XBOXONE || UNITY_ANDROID)
		public VideoPlayer movTexture;
	#endif

	// Use this for initialization
	void Start () {
		
		#if !(UNITY_PS4 || UNITY_IOS || UNITY_XBOXONE || UNITY_ANDROID)

			VideoPlayer t = this.GetComponent<VideoPlayer>();
             t.isLooping = true;
			t.Play();	

		#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
