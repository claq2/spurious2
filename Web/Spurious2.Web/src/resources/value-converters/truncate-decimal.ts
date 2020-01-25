export class TruncateDecimalValueConverter {
  toView(value) {
      let int = parseFloat(value);
      return int.toFixed(2);
  }

  fromView(value) {

  }
}

