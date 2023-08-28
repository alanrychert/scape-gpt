//-----------------------------------------------------------------------
// <copyright file="ObjectController.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class ObjectReaction : MonoBehaviour
{
    public SpeechTextManager speechTextManager;
    [SerializeField] private TextMeshProUGUI uIText;

    private Renderer _myRenderer;
    private Color originalColor;
    public ChatGPT chatGPT;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        _myRenderer = GetComponent<Renderer>();
        originalColor = _myRenderer.material.color;
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnterXR()
    {
        SetTransparent(true);
        
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExitXR()
    {
        SetTransparent(false);
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClickXR()
    {
        //uIText.text = "cliqueaste el cuadrado verde";
        speechTextManager.StartSpeaking("holaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
    }

    public void OnFire1PressedXR(){
        Color newColor = originalColor;
        newColor.b = 1f;
        newColor.r = 0f;
        newColor.g = 0f;
        _myRenderer.material.color = newColor;
    }

    public void OnFire2PressedXR(){
        Color newColor = originalColor;
        newColor.r = 1f;
        newColor.g = 0f;
        newColor.b = 0f;
        _myRenderer.material.color = newColor;
    }

    public void OnFire3PressedXR(){
        _myRenderer.material.color = originalColor;
    }

    public void OnJumpPressedXR(){
        Color newColor = originalColor;
        newColor.b = 0.3f;
        newColor.r = 0.3f;
        newColor.g = 0.3f;
        _myRenderer.material.color = newColor;
    }
    /// <summary>
    /// Sets this instance's material according to gazedAt status.
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void SetTransparent(bool gazedAt)
    {
        Debug.Log("cambie la transparencia");
        Color newColor = originalColor;
        newColor.a = 0.5f;
        _myRenderer.material.color = gazedAt ? newColor : originalColor;
    }
}
