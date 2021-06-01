﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using ReProcessor.Files;

namespace ReProcessor
{
    static class Extensions
    {
        internal static T GetPrivateField<T>(this object input, string fieldName)
        {
            Type type = input.GetType();
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo f = type.GetField(fieldName, bindingFlags);
            return (T)f.GetValue(input);
        }
        internal static void SetPrivateField(this object input, string fieldName, object value)
        {
            Type type = input.GetType();
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo f = type.GetField(fieldName, bindingFlags);
            f.SetValue(input, value);
        }
        internal static MainEffectController MainEffectController(this Camera cam)
        {
            return cam.gameObject.GetComponent(typeof(MainEffectController)) as MainEffectController;
        }
        internal static MainEffectContainerSO MainEffectContainerSO(this Camera cam)
        {
            return cam.gameObject.GetComponent(typeof(MainEffectController)).GetPrivateField<MainEffectContainerSO>("_mainEffectContainer");
        }
        internal static object GetCameraSetting(this Camera cam, string fieldName)
        {
            return cam.MainEffectContainerSO().mainEffect.GetPrivateField<object>(fieldName);
        }
        internal static void SetCameraSetting(this Camera cam, string fieldName, object value)
        {
            cam.MainEffectContainerSO().mainEffect.SetPrivateField(fieldName, value);
        }
        internal static void ApplySettings(this Camera cam, List<CameraSetting> camSettings)
        {
            foreach(var setting in camSettings)
            {
                if(setting.ValueType == valueType.num)
                    cam.SetCameraSetting(setting.PropertyName, System.Single.Parse(setting.Value.ToString()));
            }
        }
        internal static CameraSetting GetSetting(this List<CameraSetting> list, string property)
        {
            CameraSetting retrn = null!;
            foreach(var setting in list)
            {
                if (setting.PropertyName == property)
                    retrn = setting;
                else
                    retrn = null;
            }
            return retrn;
        }
    }
}
