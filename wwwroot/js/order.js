var slider = document.getElementById("myRange");
var output = document.getElementById("dinamicValue");
var totalPrice = document.getElementById("totalPrice");
output.innerHTML = slider.value; // Display the default slider value

// Update the current slider value (each time you drag the slider handle)
slider.oninput = function() {
    output.innerHTML = this.value;
    totalPrice.innerHTML = startPrice * this.value;
} 


const rWindow = document.querySelector('.window_order'),
btn = document.querySelector('.order_btn');

btn.addEventListener('click', showWindow);

function showWindow(){
    rWindow.style.display = 'block';
};



rWindow.addEventListener('click', (e)=>{
    if(e.target === rWindow)
        closeW();
});

function closeW(){
    rWindow.style.display = 'none';
}



$('body').on('input', 'input[type="number"][maxlength]', function(){
	if (this.value.length > this.maxLength){
		this.value = this.value.slice(0, this.maxLength);
	}
});