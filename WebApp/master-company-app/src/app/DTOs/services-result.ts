export interface ServicesResult<TResult> {
  data: TResult;
  executedSuccessfully: boolean;
  message: string;
}