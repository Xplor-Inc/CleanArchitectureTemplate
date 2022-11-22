import React from "react";
import ReactDOM from 'react-dom';

type ModelBox = {
    heading: string,
    message?: string,
    onClose?: React.MouseEventHandler<HTMLSpanElement>
    children?: React.ReactElement
}
const PopupWindow = (props: ModelBox) => {
    var element = document.getElementById('root-model') as HTMLElement;
    var body = document.getElementsByTagName('body')[0];
    body.style.overflowY = "hidden"
    return ReactDOM.createPortal(
        <div style={{overflowY: 'scroll',
            width: '100%',
            position: 'fixed',
            display: 'block',
            top: '0px',
            height: '100%',
            zIndex: 1031}}>
            <div className="text-left bg-white b-1 text-justify m-1 popup">
                <span className="closeX" onClick={(e) => {
                    props.onClose && props.onClose(e);
                    body.style.overflowY = "auto"
                }}>X</span>
                <div className='heading p-2 pb-0'>
                    <h4>
                        {props.heading}
                    </h4>
                </div>
                <div className="p-3">
                    {props.message ? props.message : ''}
                    {props.children}
                </div>
            </div>
            <div className="overlay"></div>
        </div>,
        element
    )
}

export default PopupWindow;