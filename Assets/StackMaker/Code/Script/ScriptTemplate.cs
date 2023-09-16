using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ScriptTemplate : MonoBehaviour
{
    #region VARIABLES

    #region PRIVATE

    private static bool isPrivate;

    #endregion

    #region PUBLIC

    public static bool isPublic;
    
    #endregion

    #region SERIALIZATION

    [Tooltip("Script")] [Description("Demo script")] [SerializeField]
    private string script;
    
    #endregion

    #endregion

    #region FUNCTIONS

    #region USER DEFINED PRIVATE

    /// <summary>
    /// Private scope user defined function 
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    private void UserDefinedPrivate()
    {
        throw new NotSupportedException();
    }
    
    #endregion

    #region USER DEFINED PUBLIC

    /// <summary>
    /// Public scope user defined function 
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    public void UserDefinedPublic()
    {
        throw new NotSupportedException();
    }
    
    #endregion

    #region UNITY

    /// <summary>
    /// Call on first frame start
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Start()
    {
        throw new NotImplementedException();
    }

    #endregion

    #endregion

}
