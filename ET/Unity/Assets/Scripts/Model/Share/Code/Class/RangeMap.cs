namespace ET
{
    public struct RangeMap
    {
        private float multiple;
        private float difference;

        private (float, float) srcRange;
        private (float, float) dstRange;

        public (float, float) SrcRange
        {
            get => this.srcRange;
            set
            {
                this.srcRange = value;
                this.Compute();
            }
        }

        public (float, float) DstRange
        {
            get => this.dstRange;
            set
            {
                this.dstRange = value;
                this.Compute();
            }
        }

        public RangeMap((float, float) srcRange, (float, float) dstRange)
        {
            this.multiple = 0;
            this.difference = 0;
            this.srcRange = srcRange;
            this.dstRange = dstRange;
            this.Compute();
        }

        public readonly float Map(float value) => this.multiple * value + this.difference;

        private void Compute()
        {
            float range1 = this.srcRange.Item2 - this.srcRange.Item1;
            float range2 = this.dstRange.Item2 - this.dstRange.Item1;
            this.multiple = range1 == 0 ? 0 : range2 / range1;
            this.difference = this.dstRange.Item1 - this.srcRange.Item1 * this.multiple;
        }
    }
}