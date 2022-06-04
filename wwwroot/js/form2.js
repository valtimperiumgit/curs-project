const entr_btn = document.querySelector('.btn_entr'),
reg_btn = document.querySelector('.btn_reg'),
form_reg = document.querySelector('.form_reg'),
form_entr = document.querySelector('.form_entr'),
mWindow = document.querySelector('.modal');

mWindow.style.display = 'block';
form_entr.style.display = 'block';

form_reg.style.display = 'none';
reg_btn.classList.add('nonActive');


entr_btn.addEventListener('click', ()=>{

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
  
});

