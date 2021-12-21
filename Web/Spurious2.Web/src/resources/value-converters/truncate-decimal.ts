export class TruncateDecimalValueConverter {
    toView(value: any) {
        let int = parseFloat(value);
        return int.toFixed(2);
    }

    fromView(value: any) {

    }
}

