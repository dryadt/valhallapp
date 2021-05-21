
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
let imageUrl = JSON.parse(fs.readFileSync("imageUrl.json"));

let artistList = JSON.parse(fs.readFileSync("artistList.json"));

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