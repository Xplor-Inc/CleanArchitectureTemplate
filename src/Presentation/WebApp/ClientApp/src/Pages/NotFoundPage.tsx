import React, { Component } from 'react';

const NotFoundPage = () => {
    return (
        <div style={{ display: 'grid', height: '60vh', alignContent: 'space-around' }}>
            <div className='text-danger text-center' style={{ fontSize: '50px' }}>
                You are not authorized to view this page
            </div>
        </div>
    );
}

export default NotFoundPage;