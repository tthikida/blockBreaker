﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    

    //Cached References
    Level level;
    int countBreakableBlocks;
    GameStatus gameStatus;

    //State Variables
    [SerializeField] int timesHit; // only serialized for debug purposes


    private void Start()
    {
        CountBreakableBlocks();

    }

    private void CountBreakableBlocks()
    { 
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (tag == "Breakable")
        {
            HandleHit();

        }
    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
            PlaySoundBreakBlock();
        FindObjectOfType<GameStatus>().AddToScore();
            TriggerSparkleVFX();
            
            Destroy(gameObject);
            level.BlockDestroyed();
    }

    private void PlaySoundBreakBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }


    private void TriggerSparkleVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }


}
