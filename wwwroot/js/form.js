const entr_btn = document.querySelector('.btn_entr'),
reg_btn = document.querySelector('.btn_reg'),
form_reg = document.querySelector('.form_reg'),
form_entr = document.querySelector('.form_entr'),
btns = document.querySelector('.madal_entr_btn'),
mWindow = document.querySelector('.modal');

form_entr.style.display = 'none';
form_reg.style.display = 'none';
function openModal(){
    mWindow.style.display = 'block';
    document.body.style.overflow = 'hidden';
    form_entr.style.display = 'block';
    reg_btn.classList.toggle('nonActive'); 
    // reg_btn.style.background='#4d4c4c';
    // clearInterval(modalTimerId);
}
function closeModal(){
    mWindow.style.display = 'none';
    document.body.style.overflow = '';
    form_entr.style.display = 'none';
    reg_btn.classList.remove('nonActive');
    form_reg.style.display = 'none';
    entr_btn.classList.remove('nonActive');
}
// btns.forEach(element => {element.addEventListener('click', openModal)});

btns.addEventListener('click', openModal);
// btnClose.addEventListener('click', closeModal);

mWindow.addEventListener('click', (e)=>{
    if(e.target === mWindow)
        closeModal();
});
document.addEventListener('keydown', (e)=>{
    if(e.code === "Escape" && mWindow.style.display === 'block')
        closeModal();
});

entr_btn.addEventListener('click', ()=>{
    // form_entr.classList.add('show');
    // form_reg.classList.remove('show');

    form_entr.style.display = 'block';
    form_reg.style.display = 'none';
    reg_btn.classList.add('nonActive');
    entr_btn.classList.remove('nonActive');
    // entr_btn.style.background='#37373';
});
reg_btn.addEventListener('click', ()=>{
    form_entr.style.display = 'none';
    form_reg.style.display = ' block';
    entr_btn.classList.add('nonActive');
    reg_btn.classList.remove('nonActive');
    // entr_btn.style.background='#4d4c4c';
    // .style.background='#373737';
});

