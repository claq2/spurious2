import {bindable} from 'aurelia-framework';

export class TestElement {
  @bindable value;

  valueChanged(newValue, oldValue) {

  }
}

