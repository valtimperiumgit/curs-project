const menu = document.querySelector(".menu");
const menuItems = document.querySelectorAll(".menuItem");
const hamburger= document.querySelector(".hamburger");
const closeIcon= document.querySelector(".closeIcon");
const menuIcon = document.querySelector(".menuIcon");


function toggleMenu() {
  if (menu.classList.contains("showMenu")) {
    menu.classList.remove("showMenu");
    closeIcon.style.display = "none";
    menuIcon.style.display = "block";
  } else {
    menu.classList.add("showMenu");
    closeIcon.style.display = "block";
    menuIcon.style.display = "none";
  }
}

hamburger.addEventListener("click", toggleMenu);

// menuItems.forEach( 
//   function(menuItem) { 
//     menuItem.addEventListener("click", toggleMenu);
//   }
// )

window.addEventListener('click', (e)=>{
  if(e.target != menu && e.target != menuIcon ){
    menu.classList.remove("showMenu");
    closeIcon.style.display = "none";
    menuIcon.style.display = "block";
  }
 });

 const arrow = document.querySelector('#arrow_anim'), description_link = document.querySelector('.description_link');
 description_link.addEventListener('mouseover', ()=>{
   arrow.classList.add('anim_ar');
 });
 description_link.addEventListener('mouseout', ()=>{
  arrow.classList.remove('anim_ar');
});


