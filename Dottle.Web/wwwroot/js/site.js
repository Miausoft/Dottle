$(document).ready(() => {
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

    initPostCheckBoxes();
    initPostWorkingTimes();

    //TODO: need to fix mouseleave and mouseenter
    $(document).on('mouseleave', '.rating-carts .cart-outline', (e) => {
        const el = $(e.target);
        const parent = el.parent();
        if (parent.attr('value') === "false") {
            const el = $(e.target);
            el.nextUntil().removeClass('filled');
            el.removeClass('filled');
        }
    });
    $(document).on('mouseenter', '.rating-carts .cart-outline', (e) => {
        const el = $(e.target);
        const parent = el.parent();
        if (parent.attr('value') === "false") {
            const el = $(e.target);
            el.prevUntil().addClass("filled");
            el.addClass("filled");
        }
    });  
});
