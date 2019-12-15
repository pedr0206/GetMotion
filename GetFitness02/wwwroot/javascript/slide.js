// Creating a variable called slideIndex which is equal to 1
let slideIndex = 1;
//
showSlides(slideIndex);

// Whenever prev and next button is pushed it runs this function
function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    // A variable that get the class name mySlides
    const slides = document.getElementsByClassName("mySlides");
    // Get the classname dot which it is specified in the dot.
    const dots = document.getElementsByClassName("dot");

    if (n > slides.length)
    {
        slideIndex = 1
    }

    if (n < 1)
    {
        slideIndex = slides.length
    }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    // Using a forloop
        for (i = 0; i < dots.length; i++)
    {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex-1].style.display = "block";
    dots[slideIndex-1].className += " active";
}