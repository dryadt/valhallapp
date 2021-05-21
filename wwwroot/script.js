﻿
// Get the modal
var modal = document.getElementById("myModal");
var isPageLoaded = true;
// Get the image and insert it inside the modal - use its "alt" text as a caption
var imgList = document.getElementsByTagName("dropdownImage");
var modalImg = document.getElementById("img01");
var captionText = document.getElementById("caption");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// for explosion generation
var explIndex = 0;
const maxExplIndex = 19;

// artSelection

var selectedArt = 0;
let imageUrl = [
    { credit: 1, url: 'https://media.discordapp.net/attachments/467635425553547307/720005811602063452/Starless.png?width=225&height=300' },
    { credit: 2, url: 'https://cdn.discordapp.com/attachments/482894390570909706/742330962423185469/starless-1.png' },
    { credit: 3, url: 'https://media.discordapp.net/attachments/601614220575899648/733139015825489940/calebhug.png' },
    { credit: 3, url: 'https://media.discordapp.net/attachments/601614220575899648/733139013371953240/calebboop.png' },
    { credit: 4, url: 'https://cdn.discordapp.com/attachments/482894390570909706/751013108142702652/unknown.png' },
    { credit: 3, url: 'https://cdn.discordapp.com/attachments/577586063967518826/769582547604340747/unknown-71.png' },
    { credit: 3, url: 'https://cdn.discordapp.com/attachments/598604322623848458/689494198591946874/dryadnuzzles.png' },
    { credit: 5, url: 'https://cdn.discordapp.com/attachments/482894390570909706/786159395959734282/image1.png' },
    { credit: 7, url: 'https://cdn.discordapp.com/attachments/577586063967518826/787819431463747645/starless_black.png' },
    { credit: 2, url: 'https://cdn.discordapp.com/attachments/718478016241467453/791583295372394556/bgtest.png' },
    { credit: 6, url: 'https://cdn.discordapp.com/attachments/482894390570909706/791966004607582228/Cukier49_-_Antares_and_Starless.png' },
    { credit: 8, url: 'https://cdn.discordapp.com/attachments/482894390570909706/800466954921836624/StarlessMuffinv2.png' },
    { credit: 9, url: 'https://cdn.discordapp.com/attachments/482894390570909706/799602382459633664/nostarsatall.png' },
    { credit: 10, url: 'https://cdn.discordapp.com/attachments/577586063967518826/807705446911180830/le_cert.png' },
    { credit: 7, url: 'https://cdn.discordapp.com/attachments/690831388408021043/813448241789534248/Skye_Starless.png' },
    { credit: 6, url: 'https://cdn.discordapp.com/attachments/482894390570909706/810458961244454942/Stellar_art_Cukier.jpg' },
    { credit: 7, url: 'https://cdn.discordapp.com/attachments/577586063967518826/816803046847676487/full_res_normal_heart.png' },
    { credit: 7, url: 'https://cdn.discordapp.com/attachments/742482228331675708/821137002598367272/emote2_heart_transparent2.png' },
    { credit: 7, url: 'https://cdn.discordapp.com/attachments/742482228331675708/823834471848738846/Starless_HS.png' },
    { credit: 3, url: 'https://cdn.discordapp.com/attachments/482894390570909706/823700988569452584/nuzzles1.png' },
    { credit: 11, url: 'https://cdn.discordapp.com/attachments/742482228331675708/823833604295360532/mere_comm_png_-1.png' },
    { credit: 9, url: 'https://cdn.discordapp.com/attachments/482894390570909706/841260596568522763/Screenshot_2.png' },
    //{ credit: 0, url: 'url' },
]

let artistList = [
    "Art by: <a href='https://twitter.com/'></a>",
    "Art by: <a href='https://twitter.com/RockAddictGlum'>Glum</a>",
    "Art by: <a href='https://twitter.com/Spaztiqe'>Spaz</a>",
    "Art by: Meg",
    "Art by: Mewdusa",
    "Art by: Ollie",
    "Art by: <a href='https://twitter.com/Cukier49'>Cukier</a>",
    "Art by: <a href='https://twitter.com/JumpMere'>Mere</a>",
    "Art by: InfernalBraxia",
    "Art by: Blight",
    "Art by: Aurora",
    "Art by: <a href='https://twitter.com/Kadus_UwU/media'>Kadus</a>",
]

// function

function clickCopyPath() {
    var text = imageUrl[selectedArt].url;
    navigator.clipboard.writeText(text).then(function () {
        console.log('Async: Copying to clipboard was successful! ' + imageUrl[selectedArt].url);
    }, function (err) {
        console.error('Async: Could not copy text: ' + imageUrl[selectedArt].url, err);
    });
}
function clickImage() {
    window.open(
        imageUrl[selectedArt].url,
        '_blank' // <- This is what makes it open in a new window.
    );
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}
document.addEventListener('keyup', function (e) {
    if (e.keyCode == 27) {
        modal.style.display = "none";
    }
});
//-- js image dropdown --
window.onload = initCommand;
function initCommand() {
    setInterval(generateImage, 1000);
}

function generateImage() {
    if (isPageLoaded == false) return
    var x = document.createElement("IMG");
    let randomImageID = Math.floor(Math.random() * imageUrl.length);
    x.setAttribute("src", imageUrl[randomImageID].url);
    x.setAttribute("width", "20%");
    x.setAttribute("class", "dropdownImage");
    x.setAttribute("style", `margin-left: ${Math.floor(Math.random() * 100)}%`);
    //left click
    x.onclick = function () {
        modal.style.display = "block";
        modalImg.src = this.src;
        captionText.innerHTML = artistList[imageUrl[randomImageID].credit];
        selectedArt = randomImageID;
    }
    //right click
    x.addEventListener('contextmenu', function (ev) {
        console.log(ev);
        ev.target.setAttribute("src", `asset/expl${explIndex}.gif`); //use explosion gif
        if (explIndex == maxExplIndex) //check if it is the last one
            explIndex = 0; //if yes, set back to 0
        else
            explIndex++; //else increment
        //requires expl1.gif, expl2.gif, expl3.gif, expl4.gif, expl5.gif and expl6.gif in same folder as html as is, can also use an array of explosion gif urls
        //also requires a variable explIndex to be declared with the other vars (same level as art array) and a const maxExplIndex that is the same as the length
        //if using an array, could be changed for arr.length instead
        setTimeout(() => x.remove(), 1000); //changed ev.remove() to () => x.remove()
        return false;
    }, { once: true });
    document.body.appendChild(x);
    setTimeout(() => x.remove(), 20000); //same thing here as above
}

// removes rightclick
document.addEventListener('contextmenu', function (e) {
    e.preventDefault();
});
// -- test if page is loaded --
function handleVisibilityChange() {
    if (document.hidden) {
        isPageLoaded = false;
    } else {
        isPageLoaded = true;
    }
}

document.addEventListener("visibilitychange", handleVisibilityChange, false);