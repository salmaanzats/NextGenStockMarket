import { ToastOptions } from 'ng2-toastr';

export class ToasterCustomOptions extends ToastOptions 
{
  animate = 'flyRight';
  newestOnTop = false;
  showCloseButton = true;
  timeOut= 10000000;
  progressBar = true;
  positionClass = 'toast-bottom-right';
}
