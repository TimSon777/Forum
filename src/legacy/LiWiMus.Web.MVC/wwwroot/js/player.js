function configurePlayer(allMusic) {

    const wrapper = document.querySelector(".wrapper"),
        musicImg = wrapper.querySelector(".img-area img"),
        musicName = wrapper.querySelector(".song-details .name"),
        musicArtist = wrapper.querySelector(".song-details .artist"),
        playPauseBtn = wrapper.querySelector(".play-pause"),
        prevBtn = wrapper.querySelector("#prev"),
        nextBtn = wrapper.querySelector("#next"),
        mainAudio = wrapper.querySelector("#main-audio"),
        progressArea = wrapper.querySelector(".progress-area"),
        progressBar = progressArea.querySelector(".progress-bar"),
        musicList = wrapper.querySelector(".music-list"),
        moreMusicBtn = wrapper.querySelector("#more-music"),
        closemoreMusic = musicList.querySelector("#close"),
        favBtn = wrapper.querySelector(".fav-btn"),
        lyrics = wrapper.querySelector(".song-lyrics"),
        closeLyrics = lyrics.querySelector("#close-lyrics"),
        lyricsBtn = wrapper.querySelector("#song-lyrics-btn");

    let musicIndex = Math.floor((Math.random() * allMusic.length) + 1);
    isMusicPaused = true;

    window.addEventListener("load", ()=>{
        loadMusic(musicIndex);
        playingSong();
    });

    function loadMusic(indexNumb){
        musicName.innerText = allMusic[indexNumb - 1].name;
        musicArtist.innerText = allMusic[indexNumb - 1].artist;
        musicImg.src = `${allMusic[indexNumb - 1].img}`;
        mainAudio.src = `${allMusic[indexNumb - 1].realSrc}`;
    }


//fav function
    function addToFav(){
        wrapper.classList.add("added_to_fav");
        favBtn.querySelector("i").innerText = "favorite";
    }

    function deleteFromFav(){
        wrapper.classList.remove("added_to_fav");
        favBtn.querySelector("i").innerText = "favorite_border";
    }


//play music function
    function playMusic(){
        wrapper.classList.add("paused");
        playPauseBtn.querySelector("i").innerText = "pause";
        mainAudio.play();
    }

//pause music function
    function pauseMusic(){
        wrapper.classList.remove("paused");
        playPauseBtn.querySelector("i").innerText = "play_arrow";
        mainAudio.pause();
    }

//prev music function
    function prevMusic(){
        musicIndex--; //decrement of musicIndex by 1
        //if musicIndex is less than 1 then musicIndex will be the array length so the last music play
        musicIndex < 1 ? musicIndex = allMusic.length : musicIndex = musicIndex;
        loadMusic(musicIndex);
        playMusic();
        playingSong();
    }

//next music function
    function nextMusic(){
        musicIndex++; //increment of musicIndex by 1
        //if musicIndex is greater than array length then musicIndex will be 1 so the first music play
        musicIndex > allMusic.length ? musicIndex = 1 : musicIndex = musicIndex;
        loadMusic(musicIndex);
        playMusic();
        playingSong();
    }

// play or pause button event
    playPauseBtn.addEventListener("click", ()=>{
        const isMusicPlay = wrapper.classList.contains("paused");
        //if isPlayMusic is true then call pauseMusic else call playMusic
        isMusicPlay ? pauseMusic() : playMusic();
        playingSong();
    });

//add to fav event
    favBtn.addEventListener("click", ()=>{
        const isAddedToMusic = wrapper.classList.contains("added_to_fav");
        isAddedToMusic ?  deleteFromFav() : addToFav();
    });

//prev music button event
    prevBtn.addEventListener("click", ()=>{
        prevMusic();
    });

//next music button event
    nextBtn.addEventListener("click", ()=>{
        nextMusic();
    });

// update progress bar width according to music current time
    mainAudio.addEventListener("timeupdate", (e)=>{
        const currentTime = e.target.currentTime; //getting playing song currentTime
        const duration = e.target.duration; //getting playing song total duration
        let progressWidth = (currentTime / duration) * 100;
        progressBar.style.width = `${progressWidth}%`;

        let musicCurrentTime = wrapper.querySelector(".current-time"),
            musicDuartion = wrapper.querySelector(".max-duration");
        mainAudio.addEventListener("loadeddata", ()=>{
            // update song total duration
            let mainAdDuration = mainAudio.duration;
            let totalMin = Math.floor(mainAdDuration / 60);
            let totalSec = Math.floor(mainAdDuration % 60);
            if(totalSec < 10){ //if sec is less than 10 then add 0 before it
                totalSec = `0${totalSec}`;
            }
            musicDuartion.innerText = `${totalMin}:${totalSec}`;
        });
        // update playing song current time
        let currentMin = Math.floor(currentTime / 60);
        let currentSec = Math.floor(currentTime % 60);
        if(currentSec < 10){ //if sec is less than 10 then add 0 before it
            currentSec = `0${currentSec}`;
        }
        musicCurrentTime.innerText = `${currentMin}:${currentSec}`;
    });

// update playing song currentTime on according to the progress bar width
    progressArea.addEventListener("click", (e)=>{
        let progressWidth = progressArea.clientWidth; //getting width of progress bar
        let clickedOffsetX = e.offsetX; //getting offset x value
        let songDuration = mainAudio.duration; //getting song total duration

        mainAudio.currentTime = (clickedOffsetX / progressWidth) * songDuration;
        playMusic(); //calling playMusic function
        playingSong();
    });

    mainAudio.addEventListener("ended", ()=>{
        nextMusic();
    });

//показать меню с музыкой
    lyricsBtn.addEventListener("click", ()=> {
        lyrics.classList.toggle("show");
    })

    closeLyrics.addEventListener("click", ()=>{
        lyricsBtn.click();
    });

//show music list onclick of music icon
    moreMusicBtn.addEventListener("click", ()=>{
        musicList.classList.toggle("show");
    });

    closemoreMusic.addEventListener("click", ()=>{
        moreMusicBtn.click();
    });

    const ulTag = wrapper.querySelector("ul");
// let create li tags according to array length for list
    for (let i = 0; i < allMusic.length; i++) {
        //let's pass the song name, artist from the array
        let liTag = `<li li-index="${i + 1}">
                <div class="row">
                  <span>${allMusic[i].name}</span>
                  <p>${allMusic[i].artist}</p>
                </div>
                <span id="${allMusic[i].src}" class="audio-duration">3:40</span>
                <audio class="${allMusic[i].src}" src="${allMusic[i].realSrc}"></audio>
              </li>`;
        ulTag.insertAdjacentHTML("beforeend", liTag); //inserting the li inside ul tag

        let liAudioDuartionTag = ulTag.querySelector(`#${allMusic[i].src}`);
        let liAudioTag = ulTag.querySelector(`.${allMusic[i].src}`);
        liAudioTag.addEventListener("loadeddata", ()=>{
            let duration = liAudioTag.duration;
            let totalMin = Math.floor(duration / 60);
            let totalSec = Math.floor(duration % 60);
            if(totalSec < 10){ //if sec is less than 10 then add 0 before it
                totalSec = `0${totalSec}`;
            };
            liAudioDuartionTag.innerText = `${totalMin}:${totalSec}`; //passing total duation of song
            liAudioDuartionTag.setAttribute("t-duration", `${totalMin}:${totalSec}`); //adding t-duration attribute with total duration value
        });
    }

//play particular song from the list onclick of li tag
    function playingSong(){
        const allLiTag = ulTag.querySelectorAll("li");

        for (let j = 0; j < allLiTag.length; j++) {
            let audioTag = allLiTag[j].querySelector(".audio-duration");

            if(allLiTag[j].classList.contains("playing")){
                allLiTag[j].classList.remove("playing");
                let adDuration = audioTag.getAttribute("t-duration");
                audioTag.innerText = adDuration;
            }

            //if the li tag index is equal to the musicIndex then add playing class in it
            if(allLiTag[j].getAttribute("li-index") == musicIndex){
                allLiTag[j].classList.add("playing");
                audioTag.innerText = "Playing";
            }

            allLiTag[j].setAttribute("onclick", "clicked(this)");
        }
    }

//particular li clicked function
    function clicked(element){
        let getLiIndex = element.getAttribute("li-index");
        musicIndex = getLiIndex; //updating current song index with clicked li index
        loadMusic(musicIndex);
        playMusic();
        playingSong();
    }
}

$(document).ready(() => {
    $.get({
        url: `/Music/Playlist/Json?playlistId=${$('#playlist-id').val()}`,
        success: (playlist) => {
            let allMusic = playlist.tracks.map(track => {
                return {
                    name: track.name,
                    artist: track.owners.map(o => o.name).join(' & '),
                    img: playlist.prefixFiles + track.album.coverLocation,
                    src: track.fileLocation.split('/')[1],
                    realSrc: playlist.prefixFiles + track.fileLocation,
                } 
            });
            
            configurePlayer(allMusic);
        }
    });
   
})
