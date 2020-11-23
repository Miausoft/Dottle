// https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

$(document).ready(() => {
    $('[data-confirm]').click(() => {​​​​​​​​
    if (!confirm($(this).prop("data-confirm")))
        e.preventDefault();
    });
   const initPostCheckBoxes = () => {
       const dayCheckBoxes = $('.day-check-box');
       $.each(dayCheckBoxes, (id, el) => {
           $(el).on('change', (event) => {
               const chkBox = $(event.target);
               const checkState = chkBox.parent().next().prop('hidden');
               chkBox.parent().next().prop('hidden', !checkState);
           });
       });
   }
   
   const initPostWorkingTimes = () => {
       $(".working-hour-from, .working-hour-to").combodate({
           firstItem: 'name',
           minuteStep: 1,
           customClass: 'form-control'
       });
   }
   
   const submitNewPostForm = (form, ep, method) => {
       const title = form.find('#PostTitle').prop('value');
       const phoneNumber = form.find('#PostPhoneNumber').prop('value');
       const email = form.find('#PostEmail').prop('value');
       const address = form.find('#PostAddress').prop('value');
       const description = form.find('#PostDescription').prop('value');
       
       const checkedBoxes = $('.day-check-box:checked');
       let timeSheet = [{}];
       
       $.each(checkedBoxes, (id, el) => {
           const elem = $(el);
           const comboDate = elem.parent().next().find('.combodate');
           const hrFrom = $(comboDate.get(0)).find('.hour').prop('value');
           const minFrom = $(comboDate.get(0)).find('.minute').prop('value');
           const hrTo = $(comboDate.get(1)).find('.hour').prop('value');
           const minTo = $(comboDate.get(1)).find('.minute').prop('value');
           
           timeSheet.push({
               DayName: elem.prop('id'),
               HourFrom: hrFrom,
               MinuteFrom: minFrom,
               HourTo: hrTo,
               MinuteTo: minTo
           });
       });
       
       const postModel = { jsonPost: JSON.stringify({
               Title: title,
               PhoneNumber: phoneNumber,
               Email: email,
               Address: address,
               Description: description,
               TimeSheet: JSON.stringify(timeSheet)}
           )
       };
       $.ajax({
           url: ep,
           type: method,
           data: postModel,
           success: (resp) => alert(resp)
       });
   }
   
   initPostCheckBoxes();
   initPostWorkingTimes();
   
   $('#new-post-form').submit((e) => {
       e.preventDefault();
       submitNewPostForm($(e.target), '/Posts/Create', 'POST');
   })

    $('#update-post-form').submit((e) => {
        e.preventDefault();
        const postId = $(e.target).data('post');
        submitNewPostForm($(e.target), '/Posts/Update/' + postId, 'PUT');
    })
   
});