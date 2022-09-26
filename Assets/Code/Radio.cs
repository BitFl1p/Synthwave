using UnityEngine;
using NAudio;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Collections;

public class Radio : MonoBehaviour
{
    // _________WIP__________
    public AudioImporter importer;
    public AudioSource source;
    public AudioClip[] songs;

    void Awake()
    {
        Debug.Log(Application.persistentDataPath + "/Music");
        Directory.CreateDirectory(Application.persistentDataPath + "/Music");
        ImportFolder(Application.persistentDataPath + "/Music");
        
    }

    private void OnLoaded(AudioClip clip)
    {
        songs.Append(clip);
    }
    private void Update()
    {
        if(songs.Count() > 1 && !source.isPlaying)
        {
            source.clip = songs[Random.Range(0, songs.Length)];
            source.Play();
        }
    }
    public async void ImportFolder(string path)
    {
        foreach (string file in Directory.EnumerateFiles(path))
        {
            Debug.Log(file);
            switch (Path.GetExtension(file))
            {
                case ".mp3": await new Task(() => Import(file, AudioType.MPEG)); break;
                case ".ogg": await new Task(() => Import(file, AudioType.OGGVORBIS)); break;
                case ".wav": await new Task(() => Import(file, AudioType.WAV)); break;
                case ".aiff": await new Task(() => Import(file, AudioType.AIFF)); break;
                case ".aif": await new Task(() => Import(file, AudioType.AIFF)); break;
                case ".mod": await new Task(() => Import(file, AudioType.MOD)); break;
                case ".it": await new Task(() => Import(file, AudioType.IT)); break;
                case ".s3m": await new Task(() => Import(file, AudioType.S3M)); break;
                case ".xm": await new Task(() => Import(file, AudioType.XM)); break;
            }
        }
    }
    public void Import(string path, AudioType type)
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, type);
        songs.Append(DownloadHandlerAudioClip.GetContent(www));
    }

}