export class TruncateDecimalValueConverter {
    toView(value: string) {
        const int = parseFloat(value);
        return int.toFixed(2);
    }

    fromView(value: string) {
        return "13.37";
    }
}
