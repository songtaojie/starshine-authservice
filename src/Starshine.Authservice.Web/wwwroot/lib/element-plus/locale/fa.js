/*! Element Plus v2.8.6 */

(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = typeof globalThis !== 'undefined' ? globalThis : global || self, global.ElementPlusLocaleFa = factory());
})(this, (function () { 'use strict';

  var fa = {
    name: "fa",
    el: {
      breadcrumb: {
        label: "\u0645\u0633\u06CC\u0631 \u0631\u0627\u0647\u0646\u0645\u0627"
      },
      colorpicker: {
        confirm: "\u062A\u0623\u06CC\u06CC\u062F",
        clear: "\u067E\u0627\u06A9 \u06A9\u0631\u062F\u0646",
        defaultLabel: "\u0627\u0646\u062A\u062E\u0627\u0628\u200C\u06AF\u0631 \u0631\u0646\u06AF",
        description: "\u0631\u0646\u06AF \u0641\u0639\u0644\u06CC {color} \u0627\u0633\u062A. \u0628\u0631\u0627\u06CC \u0627\u0646\u062A\u062E\u0627\u0628 \u0631\u0646\u06AF \u062C\u062F\u06CC\u062F\u060C \u0627\u06CC\u0646\u062A\u0631 \u0631\u0627 \u0641\u0634\u0627\u0631 \u062F\u0647\u06CC\u062F.",
        alphaLabel: "\u0645\u0642\u062F\u0627\u0631 \u0622\u0644\u0641\u0627 \u0631\u0627 \u0627\u0646\u062A\u062E\u0627\u0628 \u06A9\u0646\u06CC\u062F"
      },
      datepicker: {
        now: "\u0627\u06A9\u0646\u0648\u0646",
        today: "\u0627\u0645\u0631\u0648\u0632",
        cancel: "\u0644\u063A\u0648",
        clear: "\u067E\u0627\u06A9 \u06A9\u0631\u062F\u0646",
        confirm: "\u062A\u0623\u06CC\u06CC\u062F",
        dateTablePrompt: "\u0627\u0632 \u06A9\u0644\u06CC\u062F\u0647\u0627\u06CC \u062C\u0647\u062A\u200C\u062F\u0627\u0631 \u0648 \u0627\u06CC\u0646\u062A\u0631 \u0628\u0631\u0627\u06CC \u0627\u0646\u062A\u062E\u0627\u0628 \u0631\u0648\u0632 \u0645\u0627\u0647 \u0627\u0633\u062A\u0641\u0627\u062F\u0647 \u06A9\u0646\u06CC\u062F",
        monthTablePrompt: "\u0627\u0632 \u06A9\u0644\u06CC\u062F\u0647\u0627\u06CC \u062C\u0647\u062A\u200C\u062F\u0627\u0631 \u0648 \u0627\u06CC\u0646\u062A\u0631 \u0628\u0631\u0627\u06CC \u0627\u0646\u062A\u062E\u0627\u0628 \u0645\u0627\u0647 \u0627\u0633\u062A\u0641\u0627\u062F\u0647 \u06A9\u0646\u06CC\u062F",
        yearTablePrompt: "\u0627\u0632 \u06A9\u0644\u06CC\u062F\u0647\u0627\u06CC \u062C\u0647\u062A\u200C\u062F\u0627\u0631 \u0648 \u0627\u06CC\u0646\u062A\u0631 \u0628\u0631\u0627\u06CC \u0627\u0646\u062A\u062E\u0627\u0628 \u0633\u0627\u0644 \u0627\u0633\u062A\u0641\u0627\u062F\u0647 \u06A9\u0646\u06CC\u062F",
        selectedDate: "\u062A\u0627\u0631\u06CC\u062E \u0627\u0646\u062A\u062E\u0627\u0628\u200C\u0634\u062F\u0647",
        selectDate: "\u0627\u0646\u062A\u062E\u0627\u0628 \u062A\u0627\u0631\u06CC\u062E",
        selectTime: "\u0627\u0646\u062A\u062E\u0627\u0628 \u0632\u0645\u0627\u0646",
        startDate: "\u062A\u0627\u0631\u06CC\u062E \u0634\u0631\u0648\u0639",
        startTime: "\u0632\u0645\u0627\u0646 \u0634\u0631\u0648\u0639",
        endDate: "\u062A\u0627\u0631\u06CC\u062E \u067E\u0627\u06CC\u0627\u0646",
        endTime: "\u0632\u0645\u0627\u0646 \u067E\u0627\u06CC\u0627\u0646",
        prevYear: "\u0633\u0627\u0644 \u0642\u0628\u0644",
        nextYear: "\u0633\u0627\u0644 \u0628\u0639\u062F",
        prevMonth: "\u0645\u0627\u0647 \u0642\u0628\u0644",
        nextMonth: "\u0645\u0627\u0647 \u0628\u0639\u062F",
        year: "",
        month1: "\u0698\u0627\u0646\u0648\u06CC\u0647",
        month2: "\u0641\u0648\u0631\u06CC\u0647",
        month3: "\u0645\u0627\u0631\u0633",
        month4: "\u0622\u0648\u0631\u06CC\u0644",
        month5: "\u0645\u0647",
        month6: "\u0698\u0648\u0626\u0646",
        month7: "\u0698\u0648\u0626\u06CC\u0647",
        month8: "\u0627\u0648\u062A",
        month9: "\u0633\u067E\u062A\u0627\u0645\u0628\u0631",
        month10: "\u0627\u06A9\u062A\u0628\u0631",
        month11: "\u0646\u0648\u0627\u0645\u0628\u0631",
        month12: "\u062F\u0633\u0627\u0645\u0628\u0631",
        week: "\u0647\u0641\u062A\u0647",
        weeks: {
          sun: "\u06CC\u06A9\u200C\u0634\u0646\u0628\u0647",
          mon: "\u062F\u0648\u0634\u0646\u0628\u0647",
          tue: "\u0633\u0647\u200C\u0634\u0646\u0628\u0647",
          wed: "\u0686\u0647\u0627\u0631\u0634\u0646\u0628\u0647",
          thu: "\u067E\u0646\u062C\u200C\u0634\u0646\u0628\u0647",
          fri: "\u062C\u0645\u0639\u0647",
          sat: "\u0634\u0646\u0628\u0647"
        },
        weeksFull: {
          sun: "\u06CC\u06A9\u200C\u0634\u0646\u0628\u0647",
          mon: "\u062F\u0648\u0634\u0646\u0628\u0647",
          tue: "\u0633\u0647\u200C\u0634\u0646\u0628\u0647",
          wed: "\u0686\u0647\u0627\u0631\u0634\u0646\u0628\u0647",
          thu: "\u067E\u0646\u062C\u200C\u0634\u0646\u0628\u0647",
          fri: "\u062C\u0645\u0639\u0647",
          sat: "\u0634\u0646\u0628\u0647"
        },
        months: {
          jan: "\u0698\u0627\u0646\u0648\u06CC\u0647",
          feb: "\u0641\u0648\u0631\u06CC\u0647",
          mar: "\u0645\u0627\u0631\u0686",
          apr: "\u0622\u0648\u0631\u06CC\u0644",
          may: "\u0645\u0647",
          jun: "\u0698\u0648\u0626\u0646",
          jul: "\u0698\u0648\u0626\u06CC\u0647",
          aug: "\u0627\u0648\u062A",
          sep: "\u0633\u067E\u062A\u0627\u0645\u0628\u0631",
          oct: "\u0627\u06A9\u062A\u0628\u0631",
          nov: "\u0646\u0648\u0627\u0645\u0628\u0631",
          dec: "\u062F\u0633\u0627\u0645\u0628\u0631"
        }
      },
      inputNumber: {
        decrease: "\u06A9\u0627\u0647\u0634 \u0639\u062F\u062F",
        increase: "\u0627\u0641\u0632\u0627\u06CC\u0634 \u0639\u062F\u062F"
      },
      select: {
        loading: "\u062F\u0631 \u062D\u0627\u0644 \u0628\u0627\u0631\u06AF\u0630\u0627\u0631\u06CC",
        noMatch: "\u0647\u06CC\u0686 \u062F\u0627\u062F\u0647 \u0645\u0646\u0637\u0628\u0642\u06CC \u0648\u062C\u0648\u062F \u0646\u062F\u0627\u0631\u062F",
        noData: "\u062F\u0627\u062F\u0647\u200C\u0627\u06CC \u0645\u0648\u062C\u0648\u062F \u0646\u06CC\u0633\u062A",
        placeholder: "\u0627\u0646\u062A\u062E\u0627\u0628 \u06A9\u0646\u06CC\u062F"
      },
      mention: {
        loading: "\u062F\u0631 \u062D\u0627\u0644 \u0628\u0627\u0631\u06AF\u0630\u0627\u0631\u06CC"
      },
      dropdown: {
        toggleDropdown: "\u0628\u0627\u0632 \u0648 \u0628\u0633\u062A\u0647 \u06A9\u0631\u062F\u0646 \u0645\u0646\u0648\u06CC \u06A9\u0634\u0648\u06CC\u06CC"
      },
      cascader: {
        noMatch: "\u0647\u06CC\u0686 \u062F\u0627\u062F\u0647 \u0645\u0646\u0637\u0628\u0642\u06CC \u0648\u062C\u0648\u062F \u0646\u062F\u0627\u0631\u062F",
        loading: "\u062F\u0631 \u062D\u0627\u0644 \u0628\u0627\u0631\u06AF\u0630\u0627\u0631\u06CC",
        placeholder: "\u0627\u0646\u062A\u062E\u0627\u0628 \u06A9\u0646\u06CC\u062F",
        noData: "\u062F\u0627\u062F\u0647\u200C\u0627\u06CC \u0645\u0648\u062C\u0648\u062F \u0646\u06CC\u0633\u062A"
      },
      pagination: {
        goto: "\u0628\u0631\u0648 \u0628\u0647",
        pagesize: "/\u0635\u0641\u062D\u0647",
        total: "\u0645\u062C\u0645\u0648\u0639 {total}",
        pageClassifier: "",
        page: "\u0635\u0641\u062D\u0647",
        prev: "\u0628\u0631\u0648 \u0628\u0647 \u0635\u0641\u062D\u0647 \u0642\u0628\u0644\u06CC",
        next: "\u0628\u0631\u0648 \u0628\u0647 \u0635\u0641\u062D\u0647 \u0628\u0639\u062F\u06CC",
        currentPage: "\u0635\u0641\u062D\u0647 {pager}",
        prevPages: "{pager} \u0635\u0641\u062D\u0627\u062A \u0642\u0628\u0644\u06CC",
        nextPages: "{pager} \u0635\u0641\u062D\u0627\u062A \u0628\u0639\u062F\u06CC",
        deprecationWarning: "\u0627\u0633\u062A\u0641\u0627\u062F\u0647\u200C\u0647\u0627\u06CC \u0645\u0646\u0633\u0648\u062E \u0634\u0646\u0627\u0633\u0627\u06CC\u06CC \u0634\u062F\u060C \u0644\u0637\u0641\u0627\u064B \u0628\u0647 \u0645\u0633\u062A\u0646\u062F\u0627\u062A el-pagination \u0645\u0631\u0627\u062C\u0639\u0647 \u06A9\u0646\u06CC\u062F"
      },
      dialog: {
        close: "\u0628\u0633\u062A\u0646 \u0627\u06CC\u0646 \u062F\u06CC\u0627\u0644\u0648\u06AF"
      },
      drawer: {
        close: "\u0628\u0633\u062A\u0646 \u0627\u06CC\u0646 \u062F\u06CC\u0627\u0644\u0648\u06AF"
      },
      messagebox: {
        title: "\u067E\u06CC\u0627\u0645",
        confirm: "\u062A\u0623\u06CC\u06CC\u062F",
        cancel: "\u0644\u063A\u0648",
        error: "\u0648\u0631\u0648\u062F\u06CC \u0646\u0627\u0645\u0639\u062A\u0628\u0631",
        close: "\u0628\u0633\u062A\u0646 \u0627\u06CC\u0646 \u062F\u06CC\u0627\u0644\u0648\u06AF"
      },
      upload: {
        deleteTip: "\u0628\u0631\u0627\u06CC \u062D\u0630\u0641\u060C \u06A9\u0644\u06CC\u062F delete \u0631\u0627 \u0641\u0634\u0627\u0631 \u062F\u0647\u06CC\u062F",
        delete: "\u062D\u0630\u0641",
        preview: "\u067E\u06CC\u0634\u200C\u0646\u0645\u0627\u06CC\u0634",
        continue: "\u0627\u062F\u0627\u0645\u0647"
      },
      slider: {
        defaultLabel: "\u0644\u063A\u0632\u0646\u062F\u0647 \u0628\u06CC\u0646 {min} \u0648 {max}",
        defaultRangeStartLabel: "\u0627\u0646\u062A\u062E\u0627\u0628 \u0645\u0642\u062F\u0627\u0631 \u0634\u0631\u0648\u0639",
        defaultRangeEndLabel: "\u0627\u0646\u062A\u062E\u0627\u0628 \u0645\u0642\u062F\u0627\u0631 \u067E\u0627\u06CC\u0627\u0646"
      },
      table: {
        emptyText: "\u062F\u0627\u062F\u0647\u200C\u0627\u06CC \u0645\u0648\u062C\u0648\u062F \u0646\u06CC\u0633\u062A",
        confirmFilter: "\u062A\u0623\u06CC\u06CC\u062F",
        resetFilter: "\u0628\u0627\u0632\u0646\u0634\u0627\u0646\u06CC",
        clearFilter: "\u0647\u0645\u0647",
        sumText: "\u0645\u062C\u0645\u0648\u0639"
      },
      tour: {
        next: "\u0628\u0639\u062F\u06CC",
        previous: "\u0642\u0628\u0644\u06CC",
        finish: "\u067E\u0627\u06CC\u0627\u0646"
      },
      tree: {
        emptyText: "\u062F\u0627\u062F\u0647\u200C\u0627\u06CC \u0645\u0648\u062C\u0648\u062F \u0646\u06CC\u0633\u062A"
      },
      transfer: {
        noMatch: "\u062F\u0627\u062F\u0647\u200C\u0627\u06CC \u0645\u0637\u0627\u0628\u0642\u062A \u0646\u062F\u0627\u0631\u062F",
        noData: "\u062F\u0627\u062F\u0647\u200C\u0627\u06CC \u0645\u0648\u062C\u0648\u062F \u0646\u06CC\u0633\u062A",
        titles: ["\u0641\u0647\u0631\u0633\u062A \u06F1", "\u0641\u0647\u0631\u0633\u062A \u06F2"],
        filterPlaceholder: "\u06A9\u0644\u0645\u0647 \u06A9\u0644\u06CC\u062F\u06CC \u0631\u0627 \u0648\u0627\u0631\u062F \u06A9\u0646\u06CC\u062F",
        noCheckedFormat: "{total} \u0622\u06CC\u062A\u0645",
        hasCheckedFormat: "{checked}/{total} \u0627\u0646\u062A\u062E\u0627\u0628\u200C\u0634\u062F\u0647"
      },
      image: {
        error: "\u0646\u0627\u0645\u0648\u0641\u0642"
      },
      pageHeader: {
        title: "\u0628\u0627\u0632\u06AF\u0634\u062A"
      },
      popconfirm: {
        confirmButtonText: "\u0628\u0644\u0647",
        cancelButtonText: "\u062E\u06CC\u0631"
      },
      carousel: {
        leftArrow: "\u067E\u06CC\u06A9\u0627\u0646 \u0628\u0647 \u062C\u0647\u062A \u0686\u067E",
        rightArrow: "\u067E\u06CC\u06A9\u0627\u0646 \u0686\u0631\u062E\u0627\u0646 \u0628\u0647 \u062C\u0647\u062A \u0631\u0627\u0633\u062A",
        indicator: "\u0633\u0648\u0626\u06CC\u0686 \u0686\u0631\u062E\u0627\u0646 \u0628\u0647 \u0634\u0627\u062E\u0635 {index}"
      }
    }
  };

  return fa;

}));