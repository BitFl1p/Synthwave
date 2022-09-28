using UnityEngine;
using NAudio;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Radio : MonoBehaviour
{
    // _________WIP__________
    public AudioImporter importer;
    public AudioSource source;
    public List<string> songs;
    bool finishedLoading = true;

    void Awake()
    {
        Debug.Log(Application.persistentDataPath + "/Music");
        Directory.CreateDirectory(Application.persistentDataPath + "/Music");
        Directory.CreateDirectory(Application.persistentDataPath + "/Users");
        songs = ImportFolder(Application.persistentDataPath + "/Music");
        importer.Loaded += OnLoaded;
        
    }

    private void OnLoaded(AudioClip clip)
    {
        if (songs.Count() > 1 && !source.isPlaying)
        {
            source.clip = clip;
            source.Play();
            finishedLoading = true;
        }
        
    }
    private void Update()
    {
        if (finishedLoading)
        {
            if (songs != null ? songs.Count() > 1 : false)
            {
                if (source.clip)
                {
                    if (!source.isPlaying)
                    {
                        importer.Import(songs[Random.Range(0, songs.Count)]);
                        finishedLoading = false;
                    }

                }
                else
                {
                    importer.Import(songs[Random.Range(0, songs.Count)]);
                    finishedLoading = false;
                }
            }
        }
    }
    public bool TryImportFolder(string path, out List<string> output)
    {
        output = new();
        foreach (string file in Directory.EnumerateFiles(path))
        {
            Debug.Log(file);
            switch (Path.GetExtension(file))
            {
                case ".mp3":
                case ".ogg":
                case ".wav":
                case ".aiff":
                case ".aif":
                case ".mod":
                case ".it":
                case ".s3m":
                case ".xm":
                    output.Add(file);
                    break;

            }
        }
        if (output.Count == 0)
        {
            output = null;
            return false;
        }
        else return true;

    }
    public List<string> ImportFolder(string path)
    {
        List<string> output = new();
        foreach (string file in Directory.EnumerateFiles(path))
        {
            Debug.Log(file);
            switch (Path.GetExtension(file))
            {
                case ".mp3":
                case ".ogg":
                case ".wav":
                case ".aiff":
                case ".aif":
                case ".mod":
                case ".it":
                case ".s3m":
                case ".xm":
                    output.Add(file);
                    break;

            }
        }
        if (output.Count == 0) return null;
        else return output;

    }

}