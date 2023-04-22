using CommunityToolkit.Mvvm.ComponentModel;
using IconInfo.Strings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.UI;

namespace DBDIconRepository.Model;

public partial class Settings : ObservableObject
{
    public ApplicationDataContainer Configs { get; } = ApplicationData.Current.LocalSettings;

    public T Get<T>(string key, T defaultValue)
    {
        if (Configs.Values.TryGetValue(key, out object result))
        {
            return (T)result;
        }
        Configs.Values.Add(key, defaultValue);
        return defaultValue;
    }

    public bool Set<T>(T value, [CallerMemberName]string key = "")
    {
        if (Configs.Values.ContainsKey(key) && !Equals(Configs.Values[key], value))
        {
            Configs.Values[key] = value;
            return true;
        }
        else if (Configs.Values.ContainsKey(key) && Equals(Configs.Values[key], value))
        {
            return false;
        }
        else if (!Configs.Values.ContainsKey(key))
        {
            Configs.Values.Add(key, value);
            return true;
        }
        return false;
    }

    [ObservableProperty]
    private string dBDInstallationPath = string.Empty;


}