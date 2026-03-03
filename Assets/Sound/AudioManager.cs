using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Singleton pattern
    public static AudioManager instance;

    void Awake()
    {
        // Đảm bảo chỉ có 1 AudioManager tồn tại trong game
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Tạo sẵn các AudioSource cho từng âm thanh khi game bắt đầu
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop; // Quan trọng: Đảm bảo lấy thiết lập Loop từ Inspector
        }
    }

    // TỰ ĐỘNG PHÁT NHẠC NỀN KHI GAME BẮT ĐẦU
    private void Start()
    {
        // Gọi tên âm thanh là "BGM" (Background Music)
        Play("BGM");
    }

    // Hàm dùng để gọi phát nhạc từ các script khác
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Không tìm thấy âm thanh có tên: " + name);
            return;
        }
        s.source.Play();
    }
}