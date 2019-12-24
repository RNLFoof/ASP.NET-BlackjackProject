function betSliderUpdateValue()
{
    slider = document.getElementById("betSlider");
    label = document.getElementById("betSliderLabel");

    label.innerHTML = "$" + slider.value;
}